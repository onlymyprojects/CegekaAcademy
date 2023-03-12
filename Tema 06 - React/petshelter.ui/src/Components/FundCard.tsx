import { DoneAll } from "@mui/icons-material";
import { Card, CardActions, CardContent, Tooltip, Typography } from "@mui/material";
import { Fund } from "../Models/Fund";
import { DonationPopup } from "./DonationPopup";

export interface IFundCardProps {
    fund: Fund;
    onDonationAdded: () => void;
}

export const FundCard = (props: IFundCardProps) => {
    return (
        <Card sx={{ maxWidth: 345 }}>
            <CardContent>
                <Typography gutterBottom variant="h5" component="div">
                    {props.fund.name} &nbsp;
                    {
                        props.fund.status === "Closed" &&
                        <Tooltip title={`${props.fund.name} is closed.`}><DoneAll></DoneAll></Tooltip>
                    }
                </Typography>
                <Typography variant="body2" color="text.secondary">
                    {`Current amount: ${props.fund.donationAmout}`}<br />
                    {`Goal: ${props.fund.goal}`}<br />
                    {`Due date: ${props.fund.dueDate}`}
                </Typography>
            </CardContent>
            <CardActions sx={{ float: "right" }}>
                {props.fund.status === 'Active' && (
                    <DonationPopup fundId={props.fund.id} onDonationAdded={props.onDonationAdded} />
                )}
            </CardActions>
        </Card>
    );
}