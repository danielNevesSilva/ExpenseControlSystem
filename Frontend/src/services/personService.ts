 

import api from './api';
import { Person } from '../models/Person';

export interface CreatePersonDto {
  name: string;
  age: number;
}

export interface UpdatePersonDto {
  Id?: number;
  name?: string;
  age?: number;
}

export const personService = {
  // GET ALL
  getAll: async (): Promise<Person[]> => {
    try {
      const response = await api.get('/person');
      return response.data;
    } catch (error) {
      console.error('Erro ao buscar pessoas:', error);
      throw error;
    }
  },

  // GET BY ID
  getById: async (id: number): Promise<Person> => {
    try {
      const response = await api.get(`/person/${id}`);
      return response.data;
    } catch (error) {
      console.error(`Erro ao buscar pessoa ${id}:`, error);
      throw error;
    }
  },

  // CREATE
  create: async (person: CreatePersonDto): Promise<Person> => {
    try {
      const response = await api.post('/person', person);
      return response.data;
    } catch (error) {
      console.error('Erro ao criar pessoa:', error);
      throw error;
    }
  },

  // UPDATE
  update: async (id: number, person: UpdatePersonDto): Promise<Person> => {
    try {
      const response = await api.put(`/person/${id}`, person);
      return response.data;
    } catch (error) {
      console.error(`Erro ao atualizar pessoa ${id}:`, error);
      throw error;
    }
  },

  // DELETE
  delete: async (id: number): Promise<void> => {
    try {
      await api.delete(`/person/${id}`);
    } catch (error) {
      console.error(`Erro ao deletar pessoa ${id}:`, error);
      throw error;
    }
  }
};