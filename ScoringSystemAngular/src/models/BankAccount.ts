import { Bank } from './bank';
import { Customer } from './Customer';

export class BankAccount {
    bankAccountId: number;
    user: Customer;
    bank: Bank;
    cardNumber: string;
    debt: number;
    credit: number;
}