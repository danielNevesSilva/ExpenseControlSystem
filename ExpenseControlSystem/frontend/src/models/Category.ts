import { CategoryPurposes } from "../Constants/EnumCategoryPurposes";

 export interface Category {
  id: number;
  description: string;
  purpose: CategoryPurposes; // "Expense" ou "Income"
  createdAt: string;
  updatedAt?: string;
}