import { Address } from './Address';
import { Health } from './Health';
import { UsersHealth } from './UsersHealth';

export class User {
    userId: number;
    name: string;
    lastName: string;
    birthday: Date;
    email: string;
    addressId: number;
    healthId: number;
    address: Address;
    usersHealth: UsersHealth[];    
}