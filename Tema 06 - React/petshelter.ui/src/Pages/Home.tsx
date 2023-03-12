import { Container, Typography, Grid, Button, Box } from "@mui/material";
import { useNavigate } from "react-router-dom";
import { RescuePetPopup } from "../Components/RescuePetPopup";

export const Home = () => {

    const navigate = useNavigate();

    const RedirectToPets = () => {
        console.log("I wanna redirect to pets");
        navigate(`/Pets`);
    }

    const RedirectToFunds = () => {
        console.log("I wanna redirect to funds");
        navigate(`/Funds`);
    }

    return (
        <Container maxWidth="lg" sx={{ marginTop: "3rem" }}>
            <Typography gutterBottom variant="h2" component="div">
                Pet Shelter
            </Typography>
            <Grid container spacing={5} sx={{ justifyContent: "center", marginTop: "5rem", height: "10rem" }}>
                <Grid item xs={6}>
                    <Button variant="contained"
                        color="primary"
                        sx={{ width: "100%", height: "100%" }} onClick={RedirectToPets}>
                        Pets
                    </Button>
                </Grid>
                <Grid item xs={6}>
                    <Button variant="contained" color="secondary" sx={{ width: "100%", height: "100%", }} onClick={RedirectToFunds}>
                        Fundraisers
                    </Button>
                </Grid>
            </Grid>
            <Box sx={{ justifyContent: "center", marginTop: "5rem" }}>
                <Typography variant='body1'>
                    Found a stray pet on the street but cannot adopt it?
                </Typography>
                <RescuePetPopup />
            </Box>
        </Container>
    );
}