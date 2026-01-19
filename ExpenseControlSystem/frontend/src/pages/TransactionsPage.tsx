import { Snackbar } from '@mui/material';
import { Close } from '@mui/icons-material';
import React, { useEffect, useState } from 'react';
import {
  Typography,
  Box,
  Button,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  TextField,
  Alert,
  CircularProgress,
  Paper,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  IconButton,
  MenuItem,
  Chip,
  Select,
  FormControl,
  InputLabel
} from '@mui/material';
import { Add, Edit, Delete, TrendingUp, TrendingDown } from '@mui/icons-material';
import { transactionService } from '../services/transactionService';
import { categoryService } from '../services/categoryService';
import { personService } from '../services/personService';
import { Transaction } from '../models/Transaction';
import { Category } from '../models/Category';
import { Person } from '../models/Person';


const TransactionsPage = () => {
  const [editingTransaction, setEditingTransaction] = useState<Transaction | null>(null);
  const [successMessage, setSuccessMessage] = useState('');
  const [transactions, setTransactions] = useState<Transaction[]>([]);
  const [categories, setCategories] = useState<Category[]>([]);
  const [persons, setPersons] = useState<Person[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const [openDialog, setOpenDialog] = useState(false);
  const [formData, setFormData] = useState({
    id: 0,
    description: '',
    amount: '',
    type: 'Income',
    categoryId: '',
    personId: ''
  });
  // frontend/src/pages/TransactionsPage.tsx - Adicione esta função
const getFilteredCategories = () => {
  if (!formData.type) return categories;
  
  const transactionType = parseInt(formData.type);
  
  // Filtra categorias baseado no tipo da transação
  return categories.filter(category => {
    // category.purpose pode ser string ou número
    const categoryPurpose = typeof category.purpose === 'string' 
      ? parseInt(category.purpose)
      : category.purpose;
    
    // Regra: Despesa só pode usar categorias de Despesa (1 = 1)
    //        Receita só pode usar categorias de Receita (2 = 2)
    return categoryPurpose === transactionType;
  });
};

  const loadData = async () => {
    try {
      setLoading(true);
      const [transData, catData, perData] = await Promise.all([
        transactionService.getAll(),
        categoryService.getAll(),
        personService.getAll()
      ]);
      setTransactions(transData);
      setCategories(catData);
      setPersons(perData);
      setError('');
    } catch (err) {
      setError('Erro ao carregar dados');
      console.error(err);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    loadData();
  }, []);

  const getCategoryName = (id: number) => {
    const category = categories.find(c => c.id === id);
    return category ? category.description : 'N/A';
  };

  const getPersonName = (id: number) => {
    const person = persons.find(p => p.id === id);
    return person ? person.name : 'N/A';
  };

// Função para abrir diálogo de edição
const handleOpenEditDialog = (transaction: Transaction) => {
  setEditingTransaction(transaction);
  setFormData({
    id: transaction.id,
    description: transaction.description,
    amount: transaction.amount.toString(),
    type: transaction.type.toString(), // Converte número para string
    categoryId: transaction.categoryId.toString(),
    personId: transaction.personId.toString()
  });
  setOpenDialog(true);
};

// Função para resetar o formulário
const resetForm = () => {
  setFormData({
    id: 0,
    description: '',
    amount: '',
    type: '2', // Income = 2
    categoryId: '',
    personId: ''
  });
  setEditingTransaction(null);
};

// Função para abrir diálogo de criação
const handleOpenCreateDialog = () => {
  resetForm();
  setOpenDialog(true);
};

// Função DELETE atualizada
const handleDelete = async (id: number) => {
  if (!window.confirm('Tem certeza que deseja excluir esta transação?')) {
    return;
  }

  try {
    await transactionService.delete(id);
    setSuccessMessage('Transação excluída com sucesso!');
    loadData(); // Recarrega os dados
  } catch (err: any) {
    const errorMsg = err.response?.data?.message || 'Erro ao excluir transação';
    setError(errorMsg);
  }
};

// Função SUBMIT atualizada (CREATE ou UPDATE)
const handleSubmit = async (e: React.FormEvent) => {
  e.preventDefault();
  
  try {
    const transactionTypeEnum = formData.type === "2" ? 2 : 1;
    const payload = {
      id: formData.id,
      description: formData.description.trim(),
      amount: parseFloat(formData.amount),
      type: transactionTypeEnum, // Keep as stringls
      categoryId: parseInt(formData.categoryId),
      personId: parseInt(formData.personId)
    };
  const transactionType = parseInt(formData.type);
  const selectedCategory = categories.find(c => c.id.toString() === formData.categoryId);
  
  if (selectedCategory) {
    const categoryPurpose = typeof selectedCategory.purpose === 'string' 
      ? parseInt(selectedCategory.purpose)
      : selectedCategory.purpose;
    
    if (categoryPurpose !== transactionType) {
      setError(`Categoria inválida! Transação do tipo ${transactionType === 1 ? 'Despesa' : 'Receita'} 
                não pode usar categoria de ${categoryPurpose === 1 ? 'Despesa' : 'Receita'}.`);
      return;
    }
  }
    if (editingTransaction) {
      // UPDATE
      await transactionService.update(editingTransaction.id, payload);
      setSuccessMessage('Transação atualizada com sucesso!');
    } else {
      // CREATE
      await transactionService.create(payload);
      setSuccessMessage('Transação criada com sucesso!');
    }

    setOpenDialog(false);
    resetForm();
    loadData(); // Recarrega os dados
  } catch (err: any) {
    const errorMsg = err.response?.data?.message || 'Erro ao salvar transação';
    setError(errorMsg);
  }
};
  const calculateTotal = () => {
    return transactions.reduce((total, transaction) => {
      if (transaction.type === 2) {
        return total + transaction.amount;
      } else {
        return total - transaction.amount;
      }
    }, 0);
  };

  return (
    <Box>
      <Box sx={{ display: 'flex', justifyContent: 'space-between', mb: 3 }}>
        <Box>
          <Typography variant="h4">Transações</Typography>
          <Typography variant="h6" color={calculateTotal() >= 0 ? 'success.main' : 'error.main'}>
            Saldo: R$ {calculateTotal().toFixed(2)}
          </Typography>
        </Box>
        <Button
          variant="contained"
          startIcon={<Add />}
          onClick={() => setOpenDialog(true)}
        >
          Nova Transação
        </Button>
      </Box>

      {error && (
        <Alert severity="error" sx={{ mb: 2 }} onClose={() => setError('')}>
          {error}
        </Alert>
      )}

      {loading ? (
        <Box sx={{ display: 'flex', justifyContent: 'center', mt: 4 }}>
          <CircularProgress />
        </Box>
      ) : (
        <TableContainer component={Paper}>
          <Table>
            <TableHead>
              <TableRow>
                <TableCell>ID</TableCell>
                <TableCell>Descrição</TableCell>
                <TableCell>Valor</TableCell>
                <TableCell>Tipo</TableCell>
                <TableCell>Categoria</TableCell>
                <TableCell>Pessoa</TableCell>
                <TableCell>Data</TableCell>
                <TableCell>Ações</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {transactions.length === 0 ? (
                <TableRow>
                  <TableCell colSpan={8} align="center">
                    Nenhuma transação cadastrada
                  </TableCell>
                </TableRow>
              ) : (
                transactions.map((transaction) => (
                  <TableRow key={transaction.id}>
                    <TableCell>{transaction.id}</TableCell>
                    <TableCell>{transaction.description}</TableCell>
                    <TableCell>
                      R$ {transaction.amount.toFixed(2)}
                    </TableCell>
                    <TableCell>
                      <Chip
                        icon={transaction.type === 2 ? <TrendingUp /> : <TrendingDown />}
                        label={transaction.type === 2 ? 'Receita' : 'Despesa'}
                        color={transaction.type === 2 ? 'success' : 'error'}
                        size="small"
                      />
                    </TableCell>
                    <TableCell>{getCategoryName(transaction.categoryId)}</TableCell>
                    <TableCell>{getPersonName(transaction.personId)}</TableCell>
                    <TableCell>
                      {new Date(transaction.createdAt).toLocaleDateString('pt-BR')}
                    </TableCell>
                    <TableCell>
                        <IconButton 
    color="primary" 
    size="small"
    onClick={() => handleOpenEditDialog(transaction)} // Chama edição
    title="Editar"
  >
    <Edit />
  </IconButton>
                      <IconButton 
                        color="error" 
                        size="small"
                        onClick={() => handleDelete(transaction.id)}
                      >
                        <Delete />
                      </IconButton>
                    </TableCell>
                  </TableRow>
                ))
              )}
            </TableBody>
          </Table>
        </TableContainer>
      )}

      <Dialog open={openDialog} onClose={() => setOpenDialog(false)} maxWidth="sm" fullWidth>
        <form onSubmit={handleSubmit}>
          <DialogTitle>Nova Transação</DialogTitle>
          <DialogContent>
            <TextField
              autoFocus
              margin="dense"
              label="Descrição"
              fullWidth
              value={formData.description}
              onChange={(e) => setFormData({ ...formData, description: e.target.value })}
              required
              sx={{ mt: 2 }}
            />
            <TextField
              margin="dense"
              label="Valor (R$)"
              type="number"
              fullWidth
              value={formData.amount}
              onChange={(e) => setFormData({ ...formData, amount: e.target.value })}
              required
              inputProps={{ step: "0.01", min: "0.01" }}
            />
            
            <FormControl fullWidth margin="dense">
              <InputLabel>Tipo</InputLabel>
              <Select
                value={formData.type}
                label="Tipo"
                onChange={(e) => setFormData({ ...formData, type: e.target.value })}
                required
              >
                <MenuItem value="2">Receita</MenuItem>
                <MenuItem value="1">Despesa</MenuItem>
              </Select>
            </FormControl>

            <FormControl fullWidth margin="dense">
  <InputLabel>Categoria</InputLabel>
  <Select
    value={formData.categoryId}
    label="Categoria"
    onChange={(e) => setFormData({ ...formData, categoryId: e.target.value })}
    required
    disabled={getFilteredCategories().length === 0} // Desabilita se não houver categorias
  >
    <MenuItem value="">Selecione uma categoria</MenuItem>
    {getFilteredCategories().map((category) => (
      <MenuItem key={category.id} value={category.id}>
        {category.description} 
        ({category.purpose === 1 ? 'Despesa' : 'Receita'})
      </MenuItem>
    ))}
  </Select>
  {getFilteredCategories().length === 0 && (
    <Typography variant="caption" color="error" sx={{ ml: 2 }}>
      {formData.type === '1' 
        ? 'Nenhuma categoria de DESPESA cadastrada. Crie uma primeiro.' 
        : 'Nenhuma categoria de RECEITA cadastrada. Crie uma primeiro.'}
    </Typography>
  )}
</FormControl>

            <FormControl fullWidth margin="dense">
              <InputLabel>Pessoa</InputLabel>
              <Select
                value={formData.personId}
                label="Pessoa"
                onChange={(e) => setFormData({ ...formData, personId: e.target.value })}
                required
              >
                <MenuItem value="">Selecione uma pessoa</MenuItem>
                {persons.map((person) => (
                  <MenuItem key={person.id} value={person.id}>
                    {person.name} ({person.age} anos)
                  </MenuItem>
                ))}
              </Select>
            </FormControl>
          </DialogContent>
          <DialogActions>
            <Button onClick={() => setOpenDialog(false)}>Cancelar</Button>
            <Button type="submit" variant="contained">Salvar</Button>
          </DialogActions>
        </form>
      </Dialog>
    </Box>
  );
};

export default TransactionsPage;

function setEditingPerson(person: Person) {
    throw new Error('Function not implemented.');
}
