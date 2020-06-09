import { Role } from './role';

export class UserChangeRole {
    userId : number;
    name: string;
    lastName: string;
    email: string;
    role: Array<Role>;
}