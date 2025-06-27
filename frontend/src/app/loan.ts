export class Loan {
    id: number;
    loanAmount: number;
    currentBalance: number;
    status: string;
    applicant: string;

    constructor(id: number, loanAmount: number, currentBalance: number, status: string, applicant: string) {
        this.id = id;
        this.loanAmount = loanAmount;
        this.currentBalance = currentBalance;
        this.status = status;
        this.applicant = applicant;
    }
}