using PasswordHasher;

// create passwordhash
Console.Write("Enter a password: ");
var password = Console.ReadLine();
var (passwordHash, passwordSalt) = PasswordHasher.PasswordHasher.CreatePasswordHash(password!);
Console.WriteLine($"[sys]: passwordSalt: {passwordSalt}");
Console.WriteLine($"[sys]: passwordHash: {passwordHash}");

Console.WriteLine("");
Console.WriteLine("===============================================================");
Console.WriteLine("");

// verify the hash
Console.Write("Enter a password to verify: ");
var passwordToVerify = Console.ReadLine();
var result = PasswordHasher.PasswordHasher.VerifyPasswordHash(passwordToVerify!, passwordHash, passwordSalt);

if(result)
{
    Console.WriteLine("[sys]: Password is correct");
}
else
{
    Console.WriteLine("[sys]: Password is incorrect");
}
