import { User } from "./user.model";

export class UserParams {
    //why not interface? a class gives opportunity to use constructor..
    gender: string;
    minAge: number = 18;
    maxAge: number = 99;
    pageNumber: number = 1;
    pageSize: number = 3;
    orderBy = 'lastActive';

    constructor(user: User) {
        this.gender = user.gender === 'female' ? 'male' : 'female'
    }


}