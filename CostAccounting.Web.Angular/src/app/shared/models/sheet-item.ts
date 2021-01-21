import { Category } from "src/app/core/models/category";

export interface SheetItem {
    amount: number;
    category: Category;
    date: Date;
}
