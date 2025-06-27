export class Token {
    token: string;
    expires: Date;

    constructor(token: string, expires: Date) {
        this.token = token;
        this.expires = expires;
    }
}