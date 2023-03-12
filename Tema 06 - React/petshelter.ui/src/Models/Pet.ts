import type { Person } from "./Person";

export class Pet {
    id?: number;
    name: string;
    imageUrl: string;
    description: string;
    birthdate: Date;
    type: string;
    rescuer?: Person;
    adopter?: Person;
    weightInKg: number

    constructor(name: string, imageUrl: string, type: string, description:string, birthdate: Date, weightInKg: number, id?: number, rescuer?: Person, adopter?: Person) {
        this.id = id;
        this.name = name;
        this.imageUrl=imageUrl;
        this.type = type;
        this.description = description;
        this.birthdate = birthdate;
        this.rescuer = rescuer;
        this.adopter = adopter;
        this.weightInKg = weightInKg;
    }
}