// frontend/src/pages/Home.tsx - COMPATÍVEL COM MUI v7
import React, { useEffect, useState } from 'react';
import { 
  Typography, 
  Paper, 
  Grid, 
  Box, 
  Card, 
  CardContent,
  Button,
  Stack
} from '@mui/material';
import { 
  AccountBalance, 
  Category, 
  Receipt, 
  People,
  TrendingUp,
  TrendingDown
} from '@mui/icons-material';
import { Link } from 'react-router-dom';
import { dashboardService } from '../services/dashboardService';

const Home = () => {
  const [stats, setStats] = useState({
    totalPersons: 0,
    totalCategories: 0,
    totalTransactions: 0,
    totalBalance: 0
  });

const [, setLoading] = useState(true);
  const [, setError] = useState('');

  useEffect(() => {
    loadDashboardStats();
  }, []);

  const loadDashboardStats = async () => {
    try {
      setLoading(true);
      const data = await dashboardService.getStats();
      setStats(data);
      setError('');
    } catch (err) {
      setError('Erro ao carregar estatísticas do dashboard');
      console.error(err);
    } finally {
      setLoading(false);
    }
  };

  return (
    <Box>
      {/* Hero Section */}
      <Paper 
        elevation={3} 
        sx={{ 
          p: 4, 
          mb: 4, 
          background: 'linear-gradient(135deg, #667eea 0%, #764ba2 100%)',
          color: 'white'
        }}
      >
        <Typography variant="h3" gutterBottom>
          Bem-vindo ao Controle de Despesas
        </Typography>
        <Typography variant="h6" sx={{ mb: 3, opacity: 0.9 }}>
          Gerencie suas finanças de forma simples e eficiente
        </Typography>
        <Stack direction="row" spacing={2}>
          <Button 
            variant="contained" 
            color="secondary" 
            component={Link} 
            to="/transactions"
            size="large"
          >
            Nova Transação
          </Button>
          <Button 
            variant="outlined" 
            sx={{ color: 'white', borderColor: 'white' }}
            component={Link} 
            to="/persons"
            size="large"
          >
            Gerenciar Pessoas
          </Button>
        </Stack>
      </Paper>

      {/* Stats Cards - MUI v7 usa Grid2 */}
      <Grid container spacing={3} sx={{ mb: 4 }}>
        <Grid size={{ xs: 12, sm: 6, md: 3 }}>
          <Card>
            <CardContent sx={{ textAlign: 'center' }}>
              <People sx={{ fontSize: 40, color: 'primary.main', mb: 1 }} />
              <Typography variant="h4">{stats.totalPersons}</Typography>
              <Typography variant="body2" color="text.secondary">
                Pessoas
              </Typography>
            </CardContent>
          </Card>
        </Grid>
        
        <Grid size={{ xs: 12, sm: 6, md: 3 }}>
          <Card>
            <CardContent sx={{ textAlign: 'center' }}>
              <Category sx={{ fontSize: 40, color: 'secondary.main', mb: 1 }} />
              <Typography variant="h4">{stats.totalCategories}</Typography>
              <Typography variant="body2" color="text.secondary">
                Categorias
              </Typography>
            </CardContent>
          </Card>
        </Grid>
        
        <Grid size={{ xs: 12, sm: 6, md: 3 }}>
          <Card>
            <CardContent sx={{ textAlign: 'center' }}>
              <Receipt sx={{ fontSize: 40, color: 'success.main', mb: 1 }} />
              <Typography variant="h4">{stats.totalTransactions}</Typography>
              <Typography variant="body2" color="text.secondary">
                Transações
              </Typography>
            </CardContent>
          </Card>
        </Grid>
        
        <Grid size={{ xs: 12, sm: 6, md: 3 }}>
          <Card>
            <CardContent sx={{ textAlign: 'center' }}>
              <AccountBalance sx={{ fontSize: 40, color: 'warning.main', mb: 1 }} />
              <Typography variant="h4">
                R$ {stats.totalBalance.toLocaleString('pt-BR', { minimumFractionDigits: 2 })}
              </Typography>
              <Typography variant="body2" color="text.secondary">
                Saldo Total
              </Typography>
            </CardContent>
          </Card>
        </Grid>
      </Grid>

      {/* Quick Actions */}
      <Typography variant="h5" gutterBottom sx={{ mt: 4 }}>
        Ações Rápidas
      </Typography>
      <Grid container spacing={2}>
        <Grid size={{ xs: 12, md: 4 }}>
          <Paper sx={{ p: 3 }}>
            <Box sx={{ display: 'flex', alignItems: 'center', mb: 2 }}>
              <TrendingUp sx={{ mr: 1, color: 'success.main' }} />
              <Typography variant="h6">Adicionar Receita</Typography>
            </Box>
            <Typography variant="body2" color="text.secondary" paragraph>
              Registre uma nova entrada de dinheiro.
            </Typography>
            <Button variant="outlined" fullWidth component={Link} to="/transactions">
              Nova Receita
            </Button>
          </Paper>
        </Grid>
        
        <Grid size={{ xs: 12, md: 4 }}>
          <Paper sx={{ p: 3 }}>
            <Box sx={{ display: 'flex', alignItems: 'center', mb: 2 }}>
              <TrendingDown sx={{ mr: 1, color: 'error.main' }} />
              <Typography variant="h6">Registrar Despesa</Typography>
            </Box>
            <Typography variant="body2" color="text.secondary" paragraph>
              Adicione uma nova despesa ao sistema.
            </Typography>
            <Button variant="outlined" fullWidth component={Link} to="/transactions">
              Nova Despesa
            </Button>
          </Paper>
        </Grid>
        
        <Grid size={{ xs: 12, md: 4 }}>
          <Paper sx={{ p: 3 }}>
            <Box sx={{ display: 'flex', alignItems: 'center', mb: 2 }}>
              <People sx={{ mr: 1, color: 'info.main' }} />
              <Typography variant="h6">Gerenciar Pessoas</Typography>
            </Box>
            <Typography variant="body2" color="text.secondary" paragraph>
              Adicione ou edite pessoas no sistema.
            </Typography>
            <Button variant="outlined" fullWidth component={Link} to="/persons">
              Ver Pessoas
            </Button>
          </Paper>
        </Grid>
      </Grid>
    </Box>
  );
};

export default Home;