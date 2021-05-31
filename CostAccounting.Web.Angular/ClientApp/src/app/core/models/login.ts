export class Login {
    username: string;
    password: string;

    constructor(data: any) {
        Object.assign(this, data);
    }
}
