import { Category } from './category';

export class ListTransaction {
    id: number;
    amount: number;
    category: Category;
    date: Date;
    description: string;
    isIncome: boolean;
    userId: string;
    isSelected: boolean;

    constructor(data: any) {
        Object.assign(this, data);
        this.date = new Date(data.date);
    }
}
