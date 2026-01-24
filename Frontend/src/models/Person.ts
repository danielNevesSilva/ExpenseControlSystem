import { Transaction } from "./Transaction";

 export interface Person {
  id: number;
  name: string;
  age: number;
  createdAt: string;
  updatedAt?: string;
  transactions?: Transaction[];
}
