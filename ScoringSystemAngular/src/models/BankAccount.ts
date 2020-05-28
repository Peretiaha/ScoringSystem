import { Bank } from './bank';

export class BankAccount {
    bankAccountId: number;
    bank: Bank;
    cardNumber: string;
    debt: number;
    credit: number;
}