export default class User {
    id: number;
    email: string;
    firstName: string;
    lastName: string;
    isActive: boolean;
    isAdmin: boolean;
    createdAt: Date;
    updatedAt: Date;

    constructor(
        id: number,
        email: string,
        firstName: string,
        lastName: string,
        isActive: boolean,
        isAdmin: boolean,
        createdAt: Date,
        updatedAt: Date
    ) {
        this.id = id;
        this.email = email;
        this.firstName = firstName;
        this.lastName = lastName;
        this.isActive = isActive;
        this.isAdmin = isAdmin;
        this.createdAt = createdAt;
        this.updatedAt = updatedAt;
    }
}