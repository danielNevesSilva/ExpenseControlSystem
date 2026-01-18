 export interface Category {
  id: number;
  description: string;
  purpose: string; // "Expense" ou "Income"
  createdAt: string;
  updatedAt?: string;
}