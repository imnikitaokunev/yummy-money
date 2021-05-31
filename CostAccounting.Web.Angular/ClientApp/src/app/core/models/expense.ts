import { Category } from "./category";

export class Expense {
    id: number;
    amount: number;
    category: Category;
    categoryId: string;
    date: Date;

    constructor(data: any) {
        Object.assign(this, data);

        this.date = new Date(data.date);
    }
}
