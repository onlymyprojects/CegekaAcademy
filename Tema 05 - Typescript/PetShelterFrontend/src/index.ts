import { Donation } from './models/Donation';
import { Fund } from './models/Fund';
import { Person } from './models/Person';
import { Pet } from './models/Pet';
import { FundService } from './services/FundService';
import { PetService } from './services/PetService';

let petService = new PetService();
let fundService = new FundService();

var petToRescue = new Pet(
    "Maricel",
    "https://i.imgur.com/AO6wMYS.jpeg",
    "Cat",
    "AAAAA",
    new Date(),
    8,
    new Person("Costel", "1234567890123")
)

var fundToCreate = new Fund(
    "super fund",
    0,
    476,
    new Date(),
    new Date(Date.now() + 2 * 24 * 60 * 60 * 1000), // current date + 2 days (in miliseconds)
    "Active",
    new Person("Vasile", "6987410256398")
)

var donationToAdd = new Donation(
    "big donation",
    999,
    new Person("Vasile", "6987410256398"),
    fundToCreate
)

petService.rescue(petToRescue)
    .then(() =>
        petService.getAll()
            .then(pets => console.log(pets))
    );

// createFund and getAllFunds
fundService.createFund(fundToCreate)
    .then(() =>
        fundService.getAllFunds()
            .then(funds => console.log(funds))
    );

// getFundById , fund with id = 2
fundService.getFundById(2).then(fund => console.log(fund));

// delete fund , using fund id
fundService.deleteFund(19).then(fund => console.log(fund));

// addDonation for fund with id = 2 and getAllDonations
fundService.addDonation(2, donationToAdd)
    .then(() =>
        fundService.getAllDonations()
            .then(donations => console.log(donations))
    );