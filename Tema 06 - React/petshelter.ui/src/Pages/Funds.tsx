import { Box, Button, Container, Grid } from "@mui/material";
import { Fragment, useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { FundCard } from "../Components/FundCard";
import { Fund } from "../Models/Fund";
import { FundService } from "../Services/FundService";

export const Funds = () => {
    const [data, setData] = useState<Fund[]>([]);
    
    const fetchData = async (fundService: FundService) => {
        const response = await fundService.getAllFunds();
        setData(response);
    };

    useEffect(() => {
        const fundService = new FundService();
        fetchData(fundService);
    }, []);

    const handleDonationAdded = () => {
        const fundService = new FundService();
        fetchData(fundService);
    };

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
                        data.map((fund) => (
                            <Grid item key={fund.id} xs={12} sm={6} md={4}>
                                <FundCard fund={fund} onDonationAdded={handleDonationAdded}></FundCard>
                            </Grid>
                        ))
                    }
                </Grid>
            </Container>
        </Fragment>
    );
}