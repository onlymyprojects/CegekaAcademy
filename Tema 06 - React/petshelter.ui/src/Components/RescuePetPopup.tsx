import { Button, Dialog, DialogActions, DialogContent, DialogTitle, FormControl, InputLabel, MenuItem, Select, SelectChangeEvent, TextField } from "@mui/material";
import React from "react";
import { PetService } from "../Services/PetService";
import { Pet } from '../Models/Pet';
import { Person } from '../Models/Person';

export const RescuePetPopup = () => {
    const petService = new PetService();

    const [open, setOpen] = React.useState(false);

    const handleClickOpen = () => {
        setOpen(true);
    };

    const handleClose = () => {
        setOpen(false);
    };

    const [petName, setPetName] = React.useState('');
    const [description, setDescription] = React.useState('');
    const [weightInKg, setWeightInKg] = React.useState('');
    const [dateOfBirth, setDateOfBirth] = React.useState('');
    const [petType, setPetType] = React.useState('');
    const [personName, setPersonName] = React.useState('');
    const [personIdNumber, setPersonIdNumber] = React.useState('');

    const handlePetNameChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setPetName(event.target.value);
    };

    const handleDescriptionChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setDescription(event.target.value);
    };

    const handleWeightInKgChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setWeightInKg(event.target.value);
    };

    const handleDateOfBirthChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setDateOfBirth(event.target.value);
    };

    const handlePetTypeChange = (event: SelectChangeEvent<string>) => {
        setPetType(event.target.value);
    };

    const handlePersonNameChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setPersonName(event.target.value);
    };

    const handlePersonIdNumberChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setPersonIdNumber(event.target.value);
    };

    const handleRescueClick = async () => {
        const pet = new Pet(
            petName,
            "no image url",
            petType,
            description,
            new Date(dateOfBirth),
            parseFloat(weightInKg),
            undefined,
            new Person(personName, personIdNumber),
            undefined
        )

        await petService.rescue(pet);
        setOpen(false);
        alert("Pet was rescued, thank you :)");
    };

    return (
        <div>
            <Button variant="contained" color="success" onClick={handleClickOpen}>
                Rescue it!
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
                    <TextField id="standard-basic" label="Pet Name" variant="standard" value={petName} onChange={handlePetNameChange} /><br />
                    <TextField id="standard-basic" label="Description" variant="standard" value={description} onChange={handleDescriptionChange} /><br />
                    <TextField id="standard-basic" label="Weight In Kg" variant="standard" value={weightInKg} onChange={handleWeightInKgChange} /><br />
                    <TextField id="standard-basic" label="Date Of Birth YYYY-MM-DD" variant="standard" value={dateOfBirth} onChange={handleDateOfBirthChange} /><br /><br />
                    <FormControl fullWidth>
                        <InputLabel id="demo-simple-select-label">Pet Type</InputLabel>
                        <Select
                            labelId="demo-simple-select-label"
                            id="demo-simple-select"
                            value={petType}
                            label="Pet Type"
                            onChange={handlePetTypeChange}
                        >
                            <MenuItem value={"Cat"}>Cat</MenuItem>
                            <MenuItem value={"Dog"}>Dog</MenuItem>
                        </Select>
                    </FormControl>
                    <br /><br />
                    <TextField id="standard-basic" label="Your Name" variant="standard" value={personName} onChange={handlePersonNameChange} /><br />
                    <TextField id="standard-basic" label="Your Id Number" variant="standard" value={personIdNumber} onChange={handlePersonIdNumberChange} />
                </DialogContent>
                <DialogActions>
                    <Button variant="outlined" onClick={handleRescueClick}>Rescue</Button>
                    <Button onClick={handleClose}>Close</Button>
                </DialogActions>
            </Dialog>
        </div>
    );
}