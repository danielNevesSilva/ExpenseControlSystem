// pages/PersonTransactionsReport.tsx
import React, { useState, useEffect } from 'react';
import {
  Box,
  Typography,
  Paper,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  FormControl,
  InputLabel,
  Select,
  MenuItem,
  CircularProgress,
  Alert,
  Chip,
  IconButton,
  TablePagination
} from '@mui/material';
import { TrendingUp, TrendingDown, AccountBalance, Refresh } from '@mui/icons-material';
import { personService } from '../services/personService';
import { transactionService } from '../services/transactionService';
import { Person } from '../models/Person';

const PersonTransactionsReport = () => {
  const [persons, setPersons] = useState<Person[]>([]);
  const [selectedPersonId, setSelectedPersonId] = useState<number | ''>('');
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');
  const [totals, setTotals] = useState<{
    income: number;
    expense: number;
    balance: number;
  } | null>(null);
  const [page, setPage] = useState(0);
  const [rowsPerPage, setRowsPerPage] = useState(10);

  // Carrega pessoas
  useEffect(() => {
    loadPersons();
  }, []);

  // Carrega totais quando pessoa é selecionada
  useEffect(() => {
    if (selectedPersonId) {
      loadTotals(selectedPersonId);
    } else {
      setTotals(null);
    }
  }, [selectedPersonId]);

  const loadPersons = async () => {
    try {
      const data = await personService.getAll();
      setPersons(data);
    } catch (err) {
      setError('Erro ao carregar pessoas');
    }
  };

  const loadTotals = async (personId: number) => {
    setLoading(true);
    setError('');
    
    try {
      const data = await transactionService.getTotalsByPerson(personId);
      setTotals(data);
    } catch (err: any) {
      setError(err.response?.data?.message || 'Erro ao carregar totais');
      setTotals(null);
    } finally {
      setLoading(false);
    }
  };

  const getSelectedPerson = () => {
    return persons.find(p => p.id === selectedPersonId);
  };

  const handleChangePage = (event: unknown, newPage: number) => {
    setPage(newPage);
  };

  const handleChangeRowsPerPage = (event: React.ChangeEvent<HTMLInputElement>) => {
    setRowsPerPage(parseInt(event.target.value, 10));
    setPage(0);
  };

  const handleRefresh = () => {
    if (selectedPersonId) {
      loadTotals(selectedPersonId);
    }
  };

  return (
    <Box sx={{ p: 3 }}>
      <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', mb: 3 }}>
        <Typography variant="h4">
          Relatório por Pessoa
        </Typography>
        <IconButton 
          onClick={handleRefresh}
          color="primary"
          disabled={!selectedPersonId || loading}
        >
          <Refresh />
        </IconButton>
      </Box>

      {/* Seletor de Pessoa */}
      <Paper sx={{ p: 3, mb: 3 }}>
        <FormControl fullWidth>
          <InputLabel>Selecione uma Pessoa</InputLabel>
          <Select
            value={selectedPersonId}
            label="Selecione uma Pessoa"
            onChange={(e) => setSelectedPersonId(e.target.value as number)}
            disabled={loading}
          >
            <MenuItem value="">
              <em>Selecione uma pessoa</em>
            </MenuItem>
            {persons.map((person) => (
              <MenuItem key={person.id} value={person.id}>
                {person.name} ({person.age} anos)
              </MenuItem>
            ))}
          </Select>
        </FormControl>
        
        {getSelectedPerson() && (
          <Box sx={{ mt: 2, display: 'flex', alignItems: 'center', justifyContent: 'space-between' }}>
            <Typography variant="subtitle1">
              Pessoa selecionada: <strong>{getSelectedPerson()?.name}</strong>
            </Typography>
            <Typography variant="body2" color="textSecondary">
              ID: {selectedPersonId} • {getSelectedPerson()?.age} anos
            </Typography>
          </Box>
        )}
      </Paper>

      {error && (
        <Alert severity="error" sx={{ mb: 3 }} onClose={() => setError('')}>
          {error}
        </Alert>
      )}

      {loading ? (
        <Box sx={{ display: 'flex', justifyContent: 'center', p: 4 }}>
          <CircularProgress />
        </Box>
      ) : totals && selectedPersonId ? (
        <>
          {/* Resumo em TableContainer */}
          <TableContainer component={Paper} sx={{ mb: 3 }}>
            <Table>
              <TableHead>
                <TableRow sx={{ backgroundColor: 'action.hover' }}>
                  <TableCell colSpan={3}>
                    <Typography variant="h6" sx={{ display: 'flex', alignItems: 'center' }}>
                      <AccountBalance sx={{ mr: 1 }} />
                      Resumo Financeiro
                    </Typography>
                  </TableCell>
                </TableRow>
              </TableHead>
              <TableBody>
                {/* Linha de Receitas */}
                <TableRow>
                  <TableCell>
                    <Box sx={{ display: 'flex', alignItems: 'center' }}>
                      <TrendingUp color="success" sx={{ mr: 2 }} />
                      <Typography fontWeight="medium">Total Receitas</Typography>
                    </Box>
                  </TableCell>
                  <TableCell align="right">
                    <Typography variant="h6" color="success.main">
                      R$ {totals.income.toFixed(2)}
                    </Typography>
                  </TableCell>
                  <TableCell align="right" width="150">
                    <Chip 
                      label="Receita" 
                      color="success" 
                      size="small" 
                      variant="outlined"
                    />
                  </TableCell>
                </TableRow>
                
                {/* Linha de Despesas */}
                <TableRow>
                  <TableCell>
                    <Box sx={{ display: 'flex', alignItems: 'center' }}>
                      <TrendingDown color="error" sx={{ mr: 2 }} />
                      <Typography fontWeight="medium">Total Despesas</Typography>
                    </Box>
                  </TableCell>
                  <TableCell align="right">
                    <Typography variant="h6" color="error.main">
                      R$ {totals.expense.toFixed(2)}
                    </Typography>
                  </TableCell>
                  <TableCell align="right">
                    <Chip 
                      label="Despesa" 
                      color="error" 
                      size="small" 
                      variant="outlined"
                    />
                  </TableCell>
                </TableRow>
                
                {/* Linha de Saldo */}
                <TableRow sx={{ backgroundColor: 'action.selected' }}>
                  <TableCell>
                    <Box sx={{ display: 'flex', alignItems: 'center' }}>
                      <AccountBalance 
                        color={totals.balance >= 0 ? 'success' : 'error'} 
                        sx={{ mr: 2 }} 
                      />
                      <Typography fontWeight="bold">Saldo Total</Typography>
                    </Box>
                  </TableCell>
                  <TableCell align="right">
                    <Typography 
                      variant="h5" 
                      color={totals.balance >= 0 ? 'success.main' : 'error.main'}
                      fontWeight="bold"
                    >
                      R$ {totals.balance.toFixed(2)}
                    </Typography>
                  </TableCell>
                  <TableCell align="right">
                    <Chip 
                      label={totals.balance >= 0 ? 'Positivo' : 'Negativo'} 
                      color={totals.balance >= 0 ? 'success' : 'error'} 
                      size="medium"
                    />
                  </TableCell>
                </TableRow>
              </TableBody>
            </Table>
          </TableContainer>

          {/* Tabela de Detalhamento */}
          <TableContainer component={Paper}>
            <Table>
              <TableHead>
                <TableRow sx={{ backgroundColor: 'primary.light' }}>
                  <TableCell colSpan={4}>
                    <Typography variant="h6" color="white">
                      Detalhamento
                    </Typography>
                  </TableCell>
                </TableRow>
                <TableRow>
                  <TableCell><strong>Item</strong></TableCell>
                  <TableCell align="right"><strong>Receitas</strong></TableCell>
                  <TableCell align="right"><strong>Despesas</strong></TableCell>
                  <TableCell align="right"><strong>Diferença</strong></TableCell>
                </TableRow>
              </TableHead>
              <TableBody>
                {/* Você pode adicionar linhas detalhadas aqui se tiver mais dados */}
                <TableRow hover>
                  <TableCell>Total Geral</TableCell>
                  <TableCell align="right">
                    <Typography color="success.main">
                      R$ {totals.income.toFixed(2)}
                    </Typography>
                  </TableCell>
                  <TableCell align="right">
                    <Typography color="error.main">
                      R$ {totals.expense.toFixed(2)}
                    </Typography>
                  </TableCell>
                  <TableCell align="right">
                    <Typography 
                      color={totals.balance >= 0 ? 'success.main' : 'error.main'}
                      fontWeight="bold"
                    >
                      R$ {totals.balance.toFixed(2)}
                    </Typography>
                  </TableCell>
                </TableRow>
                
                {/* Linha de percentuais (opcional) */}
                <TableRow>
                  <TableCell>Proporção</TableCell>
                  <TableCell align="right">
                    <Typography color="textSecondary">
                      {totals.income > 0 ? '100%' : '0%'}
                    </Typography>
                  </TableCell>
                  <TableCell align="right">
                    <Typography color="textSecondary">
                      {totals.income > 0 
                        ? `${((totals.expense / totals.income) * 100).toFixed(1)}% das receitas`
                        : '0%'
                      }
                    </Typography>
                  </TableCell>
                  <TableCell align="right">
                    <Typography color="textSecondary">
                      {totals.income > 0 
                        ? `${((totals.balance / totals.income) * 100).toFixed(1)}% das receitas`
                        : '0%'
                      }
                    </Typography>
                  </TableCell>
                </TableRow>
              </TableBody>
            </Table>
            
            {/* Paginação (se necessário para dados futuros) */}
            <TablePagination
              rowsPerPageOptions={[5, 10, 25]}
              component="div"
              count={1} // Ajuste quando tiver mais dados
              rowsPerPage={rowsPerPage}
              page={page}
              onPageChange={handleChangePage}
              onRowsPerPageChange={handleChangeRowsPerPage}
            />
          </TableContainer>
        </>
      ) : selectedPersonId ? (
        <Alert severity="info">
          Nenhuma transação encontrada para esta pessoa.
        </Alert>
      ) : (
        <Alert severity="info">
          Selecione uma pessoa para visualizar o relatório.
        </Alert>
      )}
    </Box>
  );
};

export default PersonTransactionsReport;