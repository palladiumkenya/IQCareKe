--<asp:ListItem Text="Select" Value="0"></asp:ListItem>
--<asp:ListItem Text="Consultation" Value="1"></asp:ListItem>
--<asp:ListItem Text="Laboratory" Value="3"></asp:ListItem>
--<asp:ListItem Text="Pharmacy" Value="4"></asp:ListItem>
--<asp:ListItem Text="Triage" Value="5"></asp:ListItem>
--<asp:ListItem Text="Cashier" Value="6"></asp:ListItem>
Set Nocount On;
Go
Declare @OldWaitingList Table (ID int Not null , Name varchar(50) Not null, primary key(ID), unique (Name))
Insert Into @OldWaitingList (ID, Name)Values(1, 'Consultation');
Insert Into @OldWaitingList (ID, Name)Values(2,'Laboratory');
Insert Into @OldWaitingList (ID, Name)Values(4,'Pharmacy');
Insert Into @OldWaitingList (ID, Name)Values(5, 'Triage');
Insert Into @OldWaitingList (ID, Name)Values(6, 'Cashier');

--Select * from @OldWaitingList

Update W Set
	ListId = Q.QueueId
From dtl_WaitingList W Inner JOin
 (Select * from vw_WaitingQueue vw Inner Join @OldWaitingList W On W.Name = vw.QueueName)Q
 On W.ListID = Q.ID
 Where Q.QueueId <> w.ListID