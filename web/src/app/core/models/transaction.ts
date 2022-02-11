import { Category } from './category';

export class Transaction {
    id: number;
    amount: number;
    category: Category;
    date: Date;
    description: string;
    isIncome: boolean;
    userId: string;

    constructor(data: any) {
        Object.assign(this, data);
        this.date = new Date(data.date);
    }
}
