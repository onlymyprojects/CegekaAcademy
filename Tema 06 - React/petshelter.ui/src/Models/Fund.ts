import { Person } from "./Person";

export class Fund {
    id: number;
    name: string;
    donationAmout: number;
    goal: number;
    creationDate: Date;
    dueDate: Date;
    status: string;
    owner?: Person;

    constructor(id: number, name: string, donationAmout: number, goal: number, creationDate: Date, dueDate: Date, status: string, owner?: Person) {
        this.id = id;
        this.name = name;
        this.donationAmout = donationAmout;
        this.goal = goal;
        this.creationDate = creationDate;
        this.dueDate = dueDate;
        this.status = status;
        this.owner = owner;
    }
}