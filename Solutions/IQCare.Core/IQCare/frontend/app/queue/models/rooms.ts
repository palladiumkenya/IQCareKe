export class Rooms {
    Id: number;
    RoomName: string = '';
    Description: string = '';
    DisplayName: string = '';
    DeleteFlag:  boolean;
    Active: boolean;
    CreateDate: Date;
    UpdateDate?: Date;
    UpdatedBy?: number;
    CreatedBy: number;

}

export class InsertRoom {
    RoomName: string = '';
    Description: string = '';
    DisplayName: string = '';
}