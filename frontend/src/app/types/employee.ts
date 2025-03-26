export interface IEmployee {
    id: number;
    name: string;
    email: string;
    phone: string;
    jobTitle: string;
    gender: number; // Alterado de string para number
    departmentId: number;
    joiningDate: Date | string; // Permite Date ou String
    lastWorkingDate?: Date | string; // Pode ser opcional
    dateOfBirth: Date | string;
}

//A interface IEmployees serve para tipar os dados que você está manipulando, garantindo que os objetos tenham a estrutura esperada ao fazer requisições (GET, POST, PUT, DELETE) para o backend.