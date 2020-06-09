import { UsersHealth } from './UsersHealth';

export class Health {
    healthId: number;
    weight: number;
    arterialPressure: number;
    numberOfRespiratoryMovements: number;
    heartRate: number;
    hemoglobin: number;
    bilirubin: number;
    bloodSugar: number;
    whiteBloodCells: number;
    bodyTemperature: number;
    usersHealth: UsersHealth[];
    analizDate: Date;
}