import { Bank } from './bank';
import { User } from './User';

export class BankAccount {
    bankAccountId: number;
    user: User;
    bank: Bank;
    cardNumber: string;
    debt: number;
    credit: number;
}