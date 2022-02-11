import { ListTransaction } from './list-transaction';

export class TransactionsList {
    public pageSize: number = 0;
    public pageIndex: number = 0;
    public totalPages: number = 0;
    public totalCount: number = 0;
    public hasPreviousPage: boolean;
    public hasNextPage: boolean;
    public items: ListTransaction[];

    constructor(data: any){
        Object.assign(this, data);
    }

    public update(other: TransactionsList, mergeItems: boolean): TransactionsList {
        if(mergeItems){
            other.items = this.items.concat(other.items);
        }

        Object.assign(this, other);
        return this;
    }
}
