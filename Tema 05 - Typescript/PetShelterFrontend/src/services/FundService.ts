import axios from 'axios';
import { Fund } from '../models/Fund';
import { Person } from '../models/Person';
import { API_URL } from '../constants/apiUrl';
import { Donation } from '../models/Donation';

export class FundService {
    getAllFunds(): Promise<Fund[]> {
        return axios
            .get(API_URL + '/Funds')
            .then(response => {
                console.log(response.data);

                var fundsResponse: Fund[] = [];
                response.data.forEach((fundFromApi: IdentifiableFundDto) => {

                    let owner = fundFromApi.owner ? new Person(fundFromApi.owner.name, fundFromApi.owner.idNumber) : undefined;

                    fundsResponse.push(
                        new Fund(
                            fundFromApi.name,
                            fundFromApi.donationAmount,
                            fundFromApi.goal,
                            fundFromApi.creationDate,
                            fundFromApi.dueDate,
                            fundFromApi.status,
                            owner
                        )
                    )
                });

                return fundsResponse;
            })
    }

    getFundById(id: number): Promise<Fund> {
        return axios
            .get(API_URL + '/Funds/' + id)
            .then(response => {
                console.log(response.data);

                let owner = response.data.owner ? new Person(response.data.owner.name, response.data.owner.idNumber) : undefined;

                var fundsResponse = new Fund(
                    response.data.name,
                    response.data.donationAmount,
                    response.data.goal,
                    response.data.creationDate,
                    response.data.dueDate,
                    response.data.status,
                    owner
                )

                return fundsResponse;
            })
    }

    createFund(fund: Fund): Promise<void> {

        let createdFund: CreatedFundDto = {
            name: fund.name,
            donationAmount: fund.donationAmount,
            goal: fund.goal,
            creationDate: fund.creationDate,
            dueDate: fund.dueDate,
            status: fund.status,
            owner: {
                dateOfBirth: new Date(),
                idNumber: fund.owner?.id ?? "",
                name: fund.owner?.name ?? ""
            }
        }

        return axios.post(API_URL + '/Funds', createdFund);
    }

    deleteFund(fundId: number): Promise<void> {
        return axios.delete(API_URL + '/Funds/' + fundId + '/Delete');
    }

    getAllDonations(): Promise<Donation[]> {
        return axios
            .get(API_URL + '/Funds/Donations')
            .then(response => {
                console.log(response.data);

                var donationsResponse: Donation[] = [];

                response.data.forEach((donationFromApi: IdentifiableDonationDto) => {

                    let donor = donationFromApi.donor ? new Person(
                        donationFromApi.donor.name,
                        donationFromApi.donor.idNumber) : undefined;

                    let fund = donationFromApi.fund ? new Fund(
                        donationFromApi.fund.name,
                        donationFromApi.fund.donationAmount,
                        donationFromApi.fund.goal,
                        donationFromApi.fund.creationDate,
                        donationFromApi.fund.dueDate,
                        donationFromApi.fund.status) : undefined;

                    donationsResponse.push(
                        new Donation(
                            donationFromApi.name,
                            donationFromApi.amount,
                            donor,
                            fund
                        )
                    )
                });

                return donationsResponse;
            })
    }

    addDonation(fundId: number, donation: Donation): Promise<void> {

        let createdDonation: CreatedDonationDto = {
            name: donation.name,
            amount: donation.amount,
            donor: {
                dateOfBirth: new Date(),
                idNumber: donation.donor?.id ?? "",
                name: donation.donor?.name ?? ""
            },
            fund: {
                name: donation.fund?.name ?? "",
                donationAmount: donation.fund?.donationAmount ?? 0,
                goal: donation.fund?.goal ?? 0,
                creationDate: donation.fund?.creationDate ?? new Date(),
                dueDate: donation.fund?.dueDate ?? new Date(),
                status: donation.fund?.status ?? ""
            }
        }

        return axios.post(API_URL + '/Funds/' + fundId + '/Donate', createdDonation);
    }
}

interface FundDto {
    name: string;
    donationAmount: number;
    goal: number;
    creationDate: Date;
    dueDate: Date;
    status: string;
}

interface DonationDto {
    name: string;
    amount: number;
}

interface PersonDto {
    name: string;
    dateOfBirth: Date | undefined;
    idNumber: string;
}

interface IdentifiableFundDto extends FundDto {
    id: number;
    owner: PersonDto;
}

interface CreatedFundDto extends FundDto {
    owner: PersonDto;
}

interface CreatedDonationDto extends DonationDto {
    donor: PersonDto;
    fund: FundDto;
}

interface IdentifiableDonationDto extends DonationDto {
    id: number;
    donor: PersonDto;
    fund: FundDto;
}