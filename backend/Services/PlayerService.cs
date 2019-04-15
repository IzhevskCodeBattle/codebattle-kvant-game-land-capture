﻿using System.Collections.Generic;
using CodeBattle.PointWar.Server.Interfaces;
using CodeBattle.PointWar.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace CodeBattle.PointWar.Server.Services
{
    public class PlayerService: ICodeBattle<Player>
    {
        private readonly IMongoCollection<Player> _Player;

        public PlayerService([FromServices] IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("CodeBattle"));
            var database = client.GetDatabase("CodeBattle");
            _Player = database.GetCollection<Player>("Player");
        }

        public List<Player> Get()
        {
            return _Player.Find(player => true).ToList();
        }

        public Player Get(string id)
        {
            return _Player.Find<Player>(player => player.ID == id).FirstOrDefault();
        }

        public Player Create(Player player)
        {
            _Player.InsertOne(player);
            return player;
        }

        public void Update(string id, Player playerIn)
        {
            _Player.ReplaceOne(player => player.ID == id, playerIn);
        }

        public void Remove(Player playerIn)
        {
            _Player.DeleteOne(player => player.ID == playerIn.ID);
        }

        public void Remove(string id)
        {
            _Player.DeleteOne(player => player.ID == id);
        }

        public Player Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(int id, Player player)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(int ID)
        {
            throw new System.NotImplementedException();
        }
    }
}
