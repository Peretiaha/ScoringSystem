import { Address } from './Address';

export class User {
    userId: number;
    name: string;
    lastName: string;
    birthday: Date;
    email: string;
    addressId: number;
    healthId: number;
    address: Address;
}