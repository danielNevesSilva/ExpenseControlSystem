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
import { CategoryHelper } from '../utils/categoryHelper';
import { CategoryPurposes } from '../Constants/EnumCategoryPurposes';

const CategoriesPage = () => {
  const [editingCategory, setEditingCategory] = useState<Category | null>(null);
  const [successMessage, setSuccessMessage] = useState('');
  const [categories, setCategories] = useState<Category[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const [openDialog, setOpenDialog] = useState(false);
  const [formData, setFormData] = useState({
    id: 0,
    description: '',
    purpose: CategoryPurposes.Expense.toString()
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

 const handleOpenEditDialog = (categoryEdit: Category) => {
  setEditingCategory(categoryEdit);
  setFormData({
    id: categoryEdit.id,
    description: categoryEdit.description,
    purpose: categoryEdit.purpose.toString()
  });
  setOpenDialog(true);
};
const resetForm = () => {
  setFormData({
    id: 0,
    description: '',
    purpose: ''
  });
  setEditingCategory(null);
};

const handleSubmit = async (e: React.FormEvent) => {
  e.preventDefault();
  try {
    const payload = {
      id: formData.id,
      description: formData.description,
      purpose: parseInt(formData.purpose) as CategoryPurposes 
    };
    
    if (editingCategory) {
      await categoryService.update(editingCategory.id, payload);
      setSuccessMessage('Categoria atualizada com sucesso!');
    } else {
      await categoryService.create(payload);
      setSuccessMessage('Categoria Criada com sucesso!');
    }
    setOpenDialog(false);
    resetForm();
    loadCategories();
    // ...
  } catch (err: any) {
    const errorMsg = err.response?.data?.message || 'Erro ao salvar Categoria';
    setError(errorMsg);
  }
  


};
  const handleDelete = async (id: number) => {
    if (window.confirm('Tem certeza que deseja excluir esta categoria?')) {
      try {
        await categoryService.delete(id);
        loadCategories();
      } catch (err: any) {
        const errorMsg = err.response?.data?.message || 'Erro ao excluir Categoria';
         setError(errorMsg);
      }
    }
  };

  const purposeColors = {
    1: 'error',
    2: 'success'
  };

  return (
    <Box>
      <Box sx={{ display: 'flex', justifyContent: 'space-between', mb: 3 }}>
        <Typography variant="h4">Categorias</Typography>
        <Button
          variant="contained"
          startIcon={<Add />}
          onClick={() => {setOpenDialog(true);resetForm();}}
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
                        label={CategoryHelper.getPurposeName(category.purpose)}
                        color={category.purpose === CategoryPurposes.Expense ? 'error' : 'success'}
                        size="small"
                      />
                    </TableCell>
                    <TableCell>
                      {new Date(category.createdAt).toLocaleDateString('pt-BR')}
                    </TableCell>
                    <TableCell>
                      <IconButton 
                      color="primary"
                       size="small"
                       onClick={() => handleOpenEditDialog(category)}
                       title="Editar">
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
              <MenuItem value={CategoryPurposes.Expense}>Despesa</MenuItem>
              <MenuItem value={CategoryPurposes.Income}>Receita</MenuItem>
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