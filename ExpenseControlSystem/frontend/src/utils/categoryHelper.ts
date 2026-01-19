import { CategoryPurposes } from '../Constants/EnumCategoryPurposes';
import { TransactionTypes } from '../Constants/EnumtransactionTypes';
import { Category } from '../models/Category';

export const CategoryHelper = {
  getPurposeName: (purposeNumber: CategoryPurposes): string => {
    // Usa o Enum para mapear número para nome
    switch (purposeNumber) {
      case CategoryPurposes.Expense:
        return 'Despesa';
      case CategoryPurposes.Income:
        return 'Receita';
      default:
        return 'Unknown';
    }
  },
    getPurposeLabel: (purposeNumber: CategoryPurposes): string => {
    return purposeNumber === CategoryPurposes.Expense ? 'Despesa' : 'Receita';
  },
};