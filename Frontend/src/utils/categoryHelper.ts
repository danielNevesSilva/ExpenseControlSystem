import { CategoryPurposes } from '../Constants/EnumCategoryPurposes';

export const CategoryHelper = {
  getPurposeName: (purposeNumber: CategoryPurposes): string => {
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