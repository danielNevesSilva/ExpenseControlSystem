// frontend/src/pages/CategoriesPage.tsx
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
  Chip
} from '@mui/material';
import { Add, Edit, Delete } from '@mui/icons-material';
import { categoryService } from '../services/categoryService';
import { Category } from '../models/Category';

const CategoriesPage = () => {
  const [categories, setCategories] = useState<Category[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const [openDialog, setOpenDialog] = useState(false);
  const [formData, setFormData] = useState({
    description: '',
    purpose: 'Expense'
  });

  const loadCategories = async () => {
    try {
      setLoading(true);
      const data = await categoryService.getAll();
      setCategories(data);
      setError('');
    } catch (err) {
      setError('Erro ao carregar categorias');
      console.error(err);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    loadCategories();
  }, []);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      await categoryService.create(formData);
      setOpenDialog(false);
      setFormData({ description: '', purpose: 'Expense' });
      loadCategories();
    } catch (err) {
      setError('Erro ao criar categoria');
      console.error(err);
    }
  };

  const handleDelete = async (id: number) => {
    if (window.confirm('Tem certeza que deseja excluir esta categoria?')) {
      try {
        await categoryService.delete(id);
        loadCategories();
      } catch (err) {
        setError('Erro ao excluir categoria');
        console.error(err);
      }
    }
  };

  const purposeColors = {
    Expense: 'error',
    Income: 'success'
  };

  return (
    <Box>
      <Box sx={{ display: 'flex', justifyContent: 'space-between', mb: 3 }}>
        <Typography variant="h4">Categorias</Typography>
        <Button
          variant="contained"
          startIcon={<Add />}
          onClick={() => setOpenDialog(true)}
        >
          Nova Categoria
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
                <TableCell>Tipo</TableCell>
                <TableCell>Criado em</TableCell>
                <TableCell>Ações</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {categories.length === 0 ? (
                <TableRow>
                  <TableCell colSpan={5} align="center">
                    Nenhuma categoria cadastrada
                  </TableCell>
                </TableRow>
              ) : (
                categories.map((category) => (
                  <TableRow key={category.id}>
                    <TableCell>{category.id}</TableCell>
                    <TableCell>{category.description}</TableCell>
                    <TableCell>
                      <Chip 
                        label={category.purpose} 
                        color={purposeColors[category.purpose as keyof typeof purposeColors] as any}
                        size="small"
                      />
                    </TableCell>
                    <TableCell>
                      {new Date(category.createdAt).toLocaleDateString('pt-BR')}
                    </TableCell>
                    <TableCell>
                      <IconButton color="primary" size="small">
                        <Edit />
                      </IconButton>
                      <IconButton 
                        color="error" 
                        size="small"
                        onClick={() => handleDelete(category.id)}
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

      <Dialog open={openDialog} onClose={() => setOpenDialog(false)}>
        <form onSubmit={handleSubmit}>
          <DialogTitle>Nova Categoria</DialogTitle>
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
              select
              margin="dense"
              label="Tipo"
              fullWidth
              value={formData.purpose}
              onChange={(e) => setFormData({ ...formData, purpose: e.target.value })}
              required
            >
              <MenuItem value="Expense">Despesa</MenuItem>
              <MenuItem value="Income">Receita</MenuItem>
            </TextField>
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

export default CategoriesPage;