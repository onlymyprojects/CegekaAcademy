import axios from 'axios';
import { Person } from '../Models/Person';
import { Pet } from '../Models/Pet';
import { API_URL } from '../Constants/apiUrl';

export class PetService {

    async getAll(): Promise<Pet[]> {
        return axios
        .get(API_URL + '/Pets')
        .then(response => {
            console.log(response.data);

            var petsResponse: Pet[] = [];
            response.data.forEach((petFromApi: IdentifiablePetDto) => {

                let rescuer = petFromApi.rescuer ? new Person(petFromApi.rescuer.name, petFromApi.rescuer.idNumber) : undefined;
                
                let adopter = petFromApi.adopter ? new Person(petFromApi.adopter.name, petFromApi.adopter.idNumber) : undefined;

                petsResponse.push(
                    new Pet(
                        petFromApi.name, 
                        petFromApi.imageUrl,
                        petFromApi.type,
                        petFromApi.description,
                        petFromApi.birthDate,
                        petFromApi.weightInKg,
                        petFromApi.id,
                        rescuer,
                        adopter
                    )
                )
            });

            return petsResponse;
        })
    }

    async rescue(pet: Pet): Promise<void> {

        let rescuedPet: RescuedPetDto = {
            name: pet.name,
            description: pet.description,
            birthDate : pet.birthdate,
            imageUrl : pet.imageUrl,
            isHealthy : true,
            rescuer : {
                dateOfBirth : new Date(),
                idNumber : pet.rescuer?.id ?? "",
                name : pet.rescuer?.name ?? ""
            },
            type: pet.type,
            weightInKg: pet.weightInKg
        }

        return axios.post(API_URL + '/Pets', rescuedPet);
    }
}

interface PetDto
{
    birthDate: Date;
    description: string;
    imageUrl: string;
    isHealthy: boolean;
    name: string;
    weightInKg: number;
    type: string;
}

interface PersonDto
{
    name: string; 
    dateOfBirth: Date | undefined; 
    idNumber: string; 
}

interface IdentifiablePetDto extends PetDto
{
    id: number;
    rescuer: PersonDto;
    adopter: PersonDto;
}

interface RescuedPetDto extends PetDto
{
    rescuer: PersonDto;
}
