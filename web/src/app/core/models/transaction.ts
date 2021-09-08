import { Category } from "./category";

export interface Transaction {
    id: number;
    amount: number;
    category: Category;
    date: Date;
    description: string;
}