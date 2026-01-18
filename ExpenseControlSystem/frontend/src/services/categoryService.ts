import api from './api';
import { Category } from '../models/Category';

export interface CreateCategoryDto {
  description: string;
  purpose: string;
}

export interface UpdateCategoryDto {
  description?: string;
  purpose?: string;
}

export const categoryService = {
  getAll: async (): Promise<Category[]> => {
    const response = await api.get('/category');
    return response.data;
  },

  getById: async (id: number): Promise<Category> => {
    const response = await api.get(`/category/${id}`);
    return response.data;
  },

  create: async (category: CreateCategoryDto): Promise<Category> => {
    const response = await api.post('/category', category);
    return response.data;
  },

  update: async (id: number, category: UpdateCategoryDto): Promise<Category> => {
    const response = await api.put(`/category/${id}`, category);
    return response.data;
  },

  delete: async (id: number): Promise<void> => {
    await api.delete(`/category/${id}`);
  }
};