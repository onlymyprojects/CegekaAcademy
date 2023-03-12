import { Button, Dialog, DialogActions, DialogContent, DialogTitle, TextField } from "@mui/material";
import React from "react";
import { FundService } from "../Services/FundService";
import { Donation } from '../Models/Donation';
import { Person } from '../Models/Person';

export interface IDonationPopupProps {
    fundId: number;
    onDonationAdded: () => void;
}

export const DonationPopup = (props: IDonationPopupProps) => {
    const fundService = new FundService();

    const [open, setOpen] = React.useState(false);

    const handleClickOpen = () => {
        setOpen(true);
    };

    const handleClose = () => {
        setOpen(false);
    };

    const [donationName, setDonationNme] = React.useState('');
    const [donationAmount, setDonationAmount] = React.useState('');
    const [personName, setPersonName] = React.useState('');
    const [personIdNumber, setPersonIdNumber] = React.useState('');

    const handleDonationNameChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setDonationNme(event.target.value);
    };

    const handleAmountChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setDonationAmount(event.target.value);
    };

    const handlePersonNameChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setPersonName(event.target.value);
    };

    const handlePersonIdNumberChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setPersonIdNumber(event.target.value);
    };

    const handleDonateClick = async () => {
        const donation = new Donation(
            donationName,
            parseFloat(donationAmount),
            new Person(personName, personIdNumber)
        )

        await fundService.addDonation(props.fundId, donation);
        props.onDonationAdded();
        setOpen(false);
        alert("Donation completed successfully, thank you :)");
    };

    return (
        <div>
            <Button variant="contained" color="success" onClick={handleClickOpen}>
                Donate
            </Button>
            <Dialog
                open={open}
                onClose={handleClose}
                aria-labelledby="alert-dialog-title"
                aria-describedby="alert-dialog-description"
            >
                <DialogTitle id="alert-dialog-title">
                    {"Please introduce some info:"}
                </DialogTitle>
                <DialogContent>
                    <TextField id="standard-basic" label="Donation Name" variant="standard" value={donationName} onChange={handleDonationNameChange} /><br />
                    <TextField id="standard-basic" label="Amount" variant="standard" value={donationAmount} onChange={handleAmountChange} /><br /><br />
                    <TextField id="standard-basic" label="Your Name" variant="standard" value={personName} onChange={handlePersonNameChange} /><br />
                    <TextField id="standard-basic" label="Your Id Number" variant="standard" value={personIdNumber} onChange={handlePersonIdNumberChange} />
                </DialogContent>
                <DialogActions>
                    <Button variant="outlined" onClick={handleDonateClick}>Donate</Button>
                    <Button onClick={handleClose}>Close</Button>
                </DialogActions>
            </Dialog>
        </div>
    );
}