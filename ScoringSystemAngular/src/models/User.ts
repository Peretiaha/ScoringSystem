import { Address } from './Address';
import { UsersHealth } from './UsersHealth';
import { BankAccount } from './BankAccount';

export class User {
    userId: number;
    name: string;
    lastName: string;
    birthday: Date;
    email: string;
    photo: string;
    addressId: number;
    healthId: number;
    address: Address;
    usersHealth: UsersHealth[];    
    bankAccounts: BankAccount[];
}