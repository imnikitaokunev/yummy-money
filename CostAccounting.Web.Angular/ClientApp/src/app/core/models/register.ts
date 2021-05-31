export class Register {
    username: string;
    password: string;
    email: string;
    firstName: string;
    lastName: string;

    constructor(data: any) {
        Object.assign(this, data);
    }
}
