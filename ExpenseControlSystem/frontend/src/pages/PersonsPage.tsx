// frontend/src/pages/PersonsPage.tsx - VERSÃO COMPLETA
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
  IconButton
} from '@mui/material';
import { Add, Edit, Delete } from '@mui/icons-material';
import { personService } from '../services/personService';
import { Person } from '../models/Person';

const PersonsPage = () => {
  const [editingPerson, setEditingPerson] = useState<Person | null>(null);
  const [successMessage, setSuccessMessage] = useState('');
  const [persons, setPersons] = useState<Person[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const [openDialog, setOpenDialog] = useState(false);
  const [formData, setFormData] = useState({
    id: 0,
    name: '',
    age: ''
  });

  // Carregar pessoas
  const loadPersons = async () => {
    try {
      setLoading(true);
      const data = await personService.getAll();
      setPersons(data);
      setError('');
    } catch (err) {
      setError('Erro ao carregar pessoas. Verifique se o backend está rodando.');
      console.error(err);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    loadPersons();
  }, []);

 const handleOpenEditDialog = (person: Person) => {
  setEditingPerson(person);
  setFormData({
    id: person.id,
    name: person.name,
    age: person.age.toString()
  });
  setOpenDialog(true);
};

// Função para resetar o formulário
const resetForm = () => {
  setFormData({
    id: 0,
    name: '',
    age: ''
  });
  setEditingPerson(null);
};

// Função para abrir diálogo de criação
const handleOpenCreateDialog = () => {
  resetForm();
  setOpenDialog(true);
};

// Função DELETE atualizada
const handleDelete = async (id: number) => {
  if (!window.confirm('Tem certeza que deseja excluir esta pessoa?')) {
    return;
  }

  try {
    await personService.delete(id);
    setSuccessMessage('Pessoa excluída com sucesso!');
    loadPersons(); // Recarrega os dados
  } catch (err: any) {
    const errorMsg = err.response?.data?.message || 'Erro ao excluir pessoa';
    setError(errorMsg);
  }
};

// Função SUBMIT atualizada (CREATE ou UPDATE)
const handleSubmit = async (e: React.FormEvent) => {
  e.preventDefault();
  
  try {
    const payload = {
      id: formData.id,
      name: formData.name.trim(),
      age: parseInt(formData.age)
    };

    if (editingPerson) {
      // UPDATE
      await personService.update(editingPerson.id, payload);
      setSuccessMessage('Pessoa atualizada com sucesso!');
    } else {
      // CREATE
      await personService.create(payload);
      setSuccessMessage('Pessoa criada com sucesso!');
    }

    setOpenDialog(false);
    resetForm();
    loadPersons(); // Recarrega os dados
  } catch (err: any) {
    const errorMsg = err.response?.data?.message || 'Erro ao salvar pessoa';
    setError(errorMsg);
  }
};
  return (
    <Box>
      {/* Cabeçalho */}
      <Box sx={{ display: 'flex', justifyContent: 'space-between', mb: 3 }}>
        <Typography variant="h4">Pessoas</Typography>
        <Button
          variant="contained"
          startIcon={<Add />}
          onClick={() => setOpenDialog(true)}
        >
          Nova Pessoa
        </Button>
      </Box>

      {/* Mensagens de erro/sucesso */}
      {error && (
        <Alert severity="error" sx={{ mb: 2 }} onClose={() => setError('')}>
          {error}
        </Alert>
      )}

      {/* Loading */}
      {loading ? (
        <Box sx={{ display: 'flex', justifyContent: 'center', mt: 4 }}>
          <CircularProgress />
        </Box>
      ) : (
        /* Tabela de pessoas */
        <TableContainer component={Paper}>
          <Table>
            <TableHead>
              <TableRow>
                <TableCell>ID</TableCell>
                <TableCell>Nome</TableCell>
                <TableCell>Idade</TableCell>
                <TableCell>Criado em</TableCell>
                <TableCell>Ações</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {persons.length === 0 ? (
                <TableRow>
                  <TableCell colSpan={5} align="center">
                    Nenhuma pessoa cadastrada
                  </TableCell>
                </TableRow>
              ) : (
                persons.map((person) => (
                  <TableRow key={person.id}>
                    <TableCell>{person.id}</TableCell>
                    <TableCell>{person.name}</TableCell>
                    <TableCell>{person.age}</TableCell>
                    <TableCell>
                      {new Date(person.createdAt).toLocaleDateString('pt-BR')}
                    </TableCell>
                    <TableCell>
  <IconButton 
    color="primary" 
    size="small"
    onClick={() => handleOpenEditDialog(person)} // Chama edição
    title="Editar"
  >
    <Edit />
  </IconButton>
  <IconButton 
    color="error" 
    size="small"
    onClick={() => handleDelete(person.id)} // Chama delete
    title="Excluir"
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

      {/* Diálogo para criar nova pessoa */}
      <Dialog 
  open={openDialog} 
  onClose={() => {
    setOpenDialog(false);
    resetForm();
  }}
>
        <form onSubmit={handleSubmit}>
        <DialogTitle>
  {editingPerson ? 'Editar Pessoa' : 'Nova Pessoa'}
</DialogTitle>
          <DialogContent>
            <TextField
              autoFocus
              margin="dense"
              label="Nome"
              fullWidth
              value={formData.name}
              onChange={(e) => setFormData({ ...formData, name: e.target.value })}
              required
              sx={{ mt: 2 }}
            />
            <TextField
              margin="dense"
              label="Idade"
              type="number"
              fullWidth
              value={formData.age}
              onChange={(e) => setFormData({ ...formData, age: e.target.value })}
              required
              inputProps={{ min: 1 }}
            />
          </DialogContent>
          <DialogActions>
            <Button onClick={() => setOpenDialog(false)}>Cancelar</Button>
            <Button type="submit" variant="contained">
  {editingPerson ? 'Atualizar' : 'Salvar'}
</Button>
          </DialogActions>
        </form>
      </Dialog>
    </Box>
  );
};

export default PersonsPage;