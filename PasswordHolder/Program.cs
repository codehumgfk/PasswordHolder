// See https://aka.ms/new-console-template for more information
using CryptoUtil;
using PasswordHolder;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.Json;
using PasswordHolder.Core;
using System.Runtime.CompilerServices;

var secKey = "PasswordHolderApplication";
var filename = "PasswordHolder.txt";

try
{
    var actionList = ArgumentParser.Parse(args);
    if (actionList.Count == 0)
    {
        Console.WriteLine(ActionExecutor.HelpText);
        return;
    }
    
    if (!Path.Exists(filename)) File.Create(filename).Close();
    
    var contents = File.ReadAllText(filename);
    var cryptor = new Cryptor(ECryptoAlgorithm.AES256, secKey);
    var jsonstr = cryptor.Decrypt(contents);
    
    List<WebAccountPOCO> original;
    if (jsonstr == string.Empty) original= new List<WebAccountPOCO>();
    else original = JsonSerializer.Deserialize<List<WebAccountPOCO>>(jsonstr) ?? new List<WebAccountPOCO>();
    
    var executor = new ActionExecutor(original);
    executor.Execute(actionList);
    jsonstr = JsonSerializer.Serialize(executor.Result);
    contents = cryptor.Encrypt(jsonstr);
    File.WriteAllText(filename, contents);
}
catch (Exception ex)
{ 
    Console.WriteLine($"Error: {ex.Message}");
}

