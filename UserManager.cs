using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace RealWorldIntFinal
{
	public class UserManager
	{
		Dictionary<string, string> users = new Dictionary<string, string>();
		private XmlDocument userDoc = new XmlDocument();

		/// <summary>
		/// Constructor method
		/// </summary>
		public UserManager()
		{
		}

		/// <summary>
		/// Create new user with username and password
		/// </summary>
		/// <param name="username"></param>
		/// <param name="password"></param>
		public void createUser(string username, string password) {
			try
			{
				if (!File.Exists(Constants.UserFile))
				{
					XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
					xmlWriterSettings.Indent = true;
					xmlWriterSettings.NewLineOnAttributes = true;
					using(XmlWriter xmlWriter = XmlWriter.Create(Constants.UserFile, xmlWriterSettings))
					{
						xmlWriter.WriteStartDocument();
						xmlWriter.WriteStartElement("users");
                        xmlWriter.WriteStartElement("user");
                        xmlWriter.WriteElementString("UserName",username.ToLower());
                        xmlWriter.WriteElementString("Password", password);
						xmlWriter.WriteEndElement();

                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteEndDocument();
						xmlWriter.Flush();
						xmlWriter.Close();
                    }
				}
				else
				{
					XDocument xDocument = XDocument.Load(Constants.UserFile);
					XElement root = xDocument.Element("users");
					IEnumerable<XElement> rows = root.Descendants("user");
					XElement firstRow = rows.First();
					firstRow.AddBeforeSelf(
						new XElement("user",
						new XElement("UserName", username.ToLower()),
						new XElement("Password", password)));
					xDocument.Save(Constants.UserFile);
				}
			}
			catch
			{
				throw new XmlException("Error in CreateUser");
			}
		}

		/// <summary>
		/// User login
		/// </summary>
		/// <param name="username"></param>
		/// <param name="password"></param>
		/// <returns></returns>
		public User login(string username, string password) {
			try
			{
				User user = null;
				userDoc.Load(Constants.UserFile);
				XmlNodeList dataNodes = userDoc.SelectNodes("//user");
				foreach(XmlNode node in dataNodes)
				{
					int count = 0;
					string userName = "";
					string paswrd = "";
					foreach(XmlNode childNode in node.ChildNodes)
					{
						count++;
						if(count == 1)
						{
							userName = childNode.InnerText;
						}
						if(count== 2)
						{
							paswrd = childNode.InnerText;
						}
						if(userName == username.ToLower() && paswrd==password)
						{
							return user = new User(userName, paswrd);
						}
					}
				}
				return user;
			}
			catch
			{
				throw new Exception("Error in login.");
			}
		}

		/// <summary>
		/// User logout
		/// </summary>
		/// <param name="user"></param>
		public void logout(User user) {

		}

		/// <summary>
		/// load users from xml file
		/// </summary>
		/// <param name="filePath"></param>
		public void loadUsersFromXml(string filePath) {

		}
	}
}

