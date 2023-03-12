import type { Fund } from "./Fund";
import type { Person } from "./Person";

export class Donation {
    name: string;
    amount: number;
    donor?: Person;
    fund?: Fund;

    constructor(name: string, amount: number, donor?: Person, fund?: Fund) {
        this.name = name;
        this.amount = amount;
        this.donor = donor;
        this.fund = fund;
    }
}