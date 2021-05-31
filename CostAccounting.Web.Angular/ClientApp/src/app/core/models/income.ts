import { Category } from "./category";

export class Income {
    id: number;
    amount: number;
    category: Category;
    date: Date;

    constructor(data: any) {
        Object.assign(this, data);

        this.date = new Date(data.date);
    }
}
