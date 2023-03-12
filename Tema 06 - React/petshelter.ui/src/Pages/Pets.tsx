import { Box, Button, Container, Grid } from "@mui/material";
import { Fragment, useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { PetCard } from "../Components/PetCard";
import { Pet } from "../Models/Pet";
import { PetService } from "../Services/PetService";

export const Pets = () => {
    const [data, setData] = useState<Pet[]>([]);

    useEffect(() => {
        const petService = new PetService();
        const fetchData = async () => {
            const response = await petService.getAll();
            setData(response);
        };
        fetchData();
    }, []);

    const handleAdopt = (pet: Pet) => {
        console.log("Someone wants to adopt " + pet.name);
    }

    return (
        <Fragment>
            <Box>
                <Button>
                    <Link to="/">Go to the home page</Link>
                </Button>
            </Box>
            <Container>

                <Grid container spacing={4}>
                    {
                        data.map((pet) => (
                            <Grid item key={pet.id} xs={12} sm={6} md={4}>
                                <PetCard pet={pet} handleAdopt={() => handleAdopt(pet)}></PetCard>
                            </Grid>
                        ))
                    }
                </Grid>
            </Container>
        </Fragment>
    );
}