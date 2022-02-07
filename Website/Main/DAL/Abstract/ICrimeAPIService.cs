using System;
using System.Collections.Generic;
using Main.Models;

namespace Main.DAL.Abstract
{
    public interface ICrimeAPIService
    {
        void SetCredentials(string token);
        List<string>GetStates();
        List<Crime> GetSafestStates(List<string> states);
    }
        
}