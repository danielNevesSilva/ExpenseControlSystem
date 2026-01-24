
import api from './api';
import { Transaction } from '../models/Transaction';

export interface CreateTransactionDto {
  description: string;
  amount: number;
  type: string | number;
  categoryId: number;
  personId: number;
}

export interface UpdateTransactionDto {
  Id?: number;
  description?: string;
  amount?: number;
  type?: string | number;
  categoryId?: number;
  personId?: number;
}

export const transactionService = {
  getAll: async (): Promise<Transaction[]> => {
    const response = await api.get('/transaction');
    return response.data;
  },

  getById: async (id: number): Promise<Transaction> => {
    const response = await api.get(`/transaction/${id}`);
    return response.data;
  },

  create: async (transaction: CreateTransactionDto): Promise<Transaction> => {
    const response = await api.post('/transaction', transaction);
    return response.data;
  },

  update: async (id: number, transaction: UpdateTransactionDto): Promise<Transaction> => {
    const response = await api.put(`/transaction/${id}`, transaction);
    return response.data;
  },

  delete: async (id: number): Promise<void> => {
    await api.delete(`/transaction/${id}`);
  },

  // Métodos adicionais
  getByPerson: async (personId: number): Promise<Transaction[]> => {
    const response = await api.get(`/transaction/person/${personId}`);
    return response.data;
  },

  getByCategory: async (categoryId: number): Promise<Transaction[]> => {
    const response = await api.get(`/transaction/category/${categoryId}`);
    return response.data;
  },
   getTotalIncomeByPerson: async (personId: number): Promise<number> => {
    const response = await api.get(`/transaction/person/${personId}/total-income`);
    return response.data;
  },
  
  getTotalExpenseByPerson: async (personId: number): Promise<number> => {
    const response = await api.get(`/transaction/person/${personId}/total-expense`);
    return response.data;
  },
  
  // Método combinado para buscar ambos
  getTotalsByPerson: async (personId: number): Promise<{ income: number, expense: number, balance: number }> => {
    try {
      const [income, expense] = await Promise.all([
        transactionService.getTotalIncomeByPerson(personId),
        transactionService.getTotalExpenseByPerson(personId)
      ]);
      
      return {
        income,
        expense,
        balance: income - expense
      };
    } catch (error) {
      console.error('Erro ao buscar totais:', error);
      throw error;
    }
  }
};
