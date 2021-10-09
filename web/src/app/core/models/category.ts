export class Category {
    id: string;
    name: string;
    description: string;
    userId: string;

    constructor(data: any) {
        Object.assign(this, data);
    }
}
