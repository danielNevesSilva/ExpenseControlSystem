// frontend/src/services/dashboardService.ts
import api from './api';

export interface DashboardStats {
  totalPersons: number;
  totalCategories: number;
  totalTransactions: number;
  totalBalance: number;
  recentTransactions: any[];
}

export const dashboardService = {
  getStats: async (): Promise<DashboardStats> => {
    try {
      // Se tiver endpoint específico para dashboard
      // const response = await api.get('/dashboard/stats');
      // return response.data;
      
      // OU faça múltiplas requisições
      const [persons, categories, transactions] = await Promise.all([
        api.get('/person'),
        api.get('/category'),
        api.get('/transaction')
      ]);
      
      // Calcula saldo total
      const totalBalance = transactions.data.reduce((total: number, transaction: any) => {
        return transaction.type === 2 // Income
          ? total + transaction.amount
          : total - transaction.amount;
      }, 0);
      
      return {
        totalPersons: persons.data.length,
        totalCategories: categories.data.length,
        totalTransactions: transactions.data.length,
        totalBalance: parseFloat(totalBalance.toFixed(2)),
        recentTransactions: transactions.data.slice(0, 5) // Últimas 5 transações
      };
    } catch (error) {
      console.error('Erro ao carregar estatísticas:', error);
      throw error;
    }
  }
};