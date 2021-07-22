export class Category {
    id: string;
    name: string;
    description: string;

    constructor(data: any) {
        Object.assign(this, data);
    }
}
