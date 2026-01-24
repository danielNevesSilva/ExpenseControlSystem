 
export interface Transaction {
  id: number;
  description: string;
  amount: number;
  type: string | number; // "Expense" ou "Income"
  categoryId: number;
  personId: number;
  createdAt: string;
  categoryDescription?: string;
  personName?: string;
}