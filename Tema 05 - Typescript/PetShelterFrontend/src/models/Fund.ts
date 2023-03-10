import type { Person } from "./Person";

export class Fund {
    name: string;
    donationAmount: number;
    goal: number;
    creationDate: Date;
    dueDate: Date;
    status: string;
    owner?: Person;

    constructor(name: string, donationAmount: number, goal: number, creationDate: Date, dueDate: Date, status: string, owner?: Person) {
        this.name = name;
        this.donationAmount = donationAmount;
        this.goal = goal;
        this.creationDate = creationDate;
        this.dueDate = dueDate;
        this.status = status;
        this.owner = owner;
    }
}